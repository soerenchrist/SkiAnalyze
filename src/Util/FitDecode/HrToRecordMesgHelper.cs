using Dynastream.Fit;
using FitDecode.Models;
using DateTime = Dynastream.Fit.DateTime;

namespace FitDecode;

internal class HrToRecordMesgHelper
{
    public static void MergeHeartRates(FitMessages messages)
    {
        float? hrAnchorEventTimestamp = 0.0f;
        DateTime hrAnchorTimestamp = new DateTime(0);
        bool hrAnchorSet = false;
        byte? lastValidHr = 0;
        DateTime lastValidHrTime = new DateTime(0);

        DateTime recordRangeStartTime = new DateTime(messages.Records[0].GetTimestamp());
        int hrStartIndex = 0;
        int hrSubIndex = 0;

        foreach (RecordMesg recordMesg in messages.Records)
        {
            long hrSum = 0;
            long hrSumCount = 0;

            // Obtain the time for which the record message is valid
            DateTime record_range_end_time = new DateTime(recordMesg.GetTimestamp());

            // Need to determine timestamp range which applies to this record
            bool findingInRangeHrMesgs = true;

            // Start searching HR mesgs where we left off
            int hrMesgCounter = hrStartIndex;
            int hrSubMesgCounter = hrSubIndex;

            //
            // Update this while() to loop through just the HR messages
            //
            while (findingInRangeHrMesgs && (hrMesgCounter < messages.HeartRates.Count))
            {
                HrMesg hrMesg = new HrMesg(messages.HeartRates[hrMesgCounter]);

                // Update HR timestamp anchor, if present
                if (hrMesg.GetTimestamp() != null && hrMesg.GetTimestamp().GetTimeStamp() != 0)
                {
                    hrAnchorTimestamp = new DateTime(hrMesg.GetTimestamp());
                    hrAnchorSet = true;

                    var fractionalTimestamp = hrMesg.GetFractionalTimestamp();

                    if (fractionalTimestamp != null)
                        hrAnchorTimestamp.Add(fractionalTimestamp.Value);

                    if (hrMesg.GetNumEventTimestamp() == 1)
                    {
                        hrAnchorEventTimestamp = hrMesg.GetEventTimestamp(0);
                    }
                    else
                    {
                        throw new FitException("FIT HrToRecordMesgBroadcastPlugin Error: Anchor HR mesg must have 1 event_timestamp");
                    }
                }

                if (hrAnchorSet == false)
                {
                    // We cannot process any HR messages if we have not received a timestamp anchor
                    throw new FitException("FIT HrToRecordMesgBroadcastPlugin Error: No anchor timestamp received in a HR mesg before diff HR mesgs");
                }
                else if (hrMesg.GetNumEventTimestamp() != hrMesg.GetNumFilteredBpm())
                {
                    throw new FitException("FIT HrToRecordMesgBroadcastPlugin Error: HR mesg with mismatching event timestamp and filtered bpm");
                }
                for (int j = hrSubMesgCounter; j < hrMesg.GetNumEventTimestamp(); j++)
                {
                    // Build up timestamp for each message using the anchor and event_timestamp
                    DateTime hrMesgTime = new DateTime(hrAnchorTimestamp);
                    float? eventTimeStamp = hrMesg.GetEventTimestamp(j);

                    // Deal with roll over case
                    if (eventTimeStamp < hrAnchorEventTimestamp)
                    {
                        if ((hrAnchorEventTimestamp - eventTimeStamp) > (1 << 21))
                        {
                            eventTimeStamp += (1 << 22);
                        }
                        else
                        {
                            throw new FitException("FIT HrToRecordMesgBroadcastPlugin Error: Anchor event_timestamp is greater than subsequent event_timestamp. This does not allow for correct delta calculation.");
                        }
                    }
                    var diff = eventTimeStamp - hrAnchorEventTimestamp;
                    if (diff != null)
                        hrMesgTime.Add((double) diff);

                    // Check if hrMesgTime is gt record start time
                    // and if hrMesgTime is lte to record end time
                    if ((hrMesgTime.CompareTo(recordRangeStartTime) > 0) &&
                       (hrMesgTime.CompareTo(record_range_end_time) <= 0))
                    {
                        var bpm = hrMesg.GetFilteredBpm(j);
                        if (bpm != null)
                            hrSum += (long)bpm;
                        hrSumCount++;
                        lastValidHrTime = new DateTime(hrMesgTime);

                    }
                    // check if hrMesgTime exceeds the record time
                    else if (hrMesgTime.CompareTo(record_range_end_time) > 0)
                    {
                        // Remember where we left off
                        hrStartIndex = hrMesgCounter;
                        hrSubIndex = j;
                        findingInRangeHrMesgs = false;

                        if (hrSumCount > 0)
                        {
                            // Update record heart rate
                            lastValidHr = (byte?)System.Math.Round((((float)hrSum) / hrSumCount), System.MidpointRounding.AwayFromZero);
                            recordMesg.SetHeartRate(lastValidHr);
                            messages.RecordFieldNames.Add("HeartRate");
                        }
                        // If no stored HR is available, fill in record messages with the
                        // last valid filtered hr for a maximum of 5 seconds
                        else if ((recordRangeStartTime.CompareTo(lastValidHrTime) > 0) &&
                                ((recordRangeStartTime.GetTimeStamp() - lastValidHrTime.GetTimeStamp()) < 5))
                        {
                            recordMesg.SetHeartRate(lastValidHr);
                            messages.RecordFieldNames.Add("HeartRate");
                        }

                        // Reset HR average
                        hrSum = 0;
                        hrSumCount = 0;

                        recordRangeStartTime = new DateTime(record_range_end_time);

                        // Breaks out of looping within the event_timestamp array
                        break;
                    }
                }

                hrMesgCounter++;
                hrSubMesgCounter = 0;
            }
        }
    }
}
