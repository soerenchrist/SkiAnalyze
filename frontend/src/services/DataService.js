import axios from 'axios';

const baseUrl = 'https://localhost:5001/api/';

export default {
  async getGondolas() {
    const response = await axios.get(`${baseUrl}gondolas?SwLat=46.905715&SwLon=10.900997&NeLat=47.000393&NeLon=11.049291`);
    return response.data;
  },
  async getPistes() {
    const response = await axios.get(`${baseUrl}pistes?SwLat=46.905715&SwLon=10.900997&NeLat=47.000393&NeLon=11.049291`);
    return response.data;
  },
  async getTracks(userSessionId) {
    let url = `${baseUrl}tracks`;
    if (userSessionId) {
      url += `?UserSessionId=${userSessionId}`;
    }
    const response = await axios.get(url);
    return response.data;
  },
  async createTrack(track) {
    const url = `${baseUrl}tracks`;
    const response = await axios.post(url, track);
    return response.data;
  },
  async removeTrack(track) {
    const url = `${baseUrl}tracks?UserSessionId=${track.userSessionId}&TrackId=${track.id}`;
    await axios.delete(url);
  },
  async startAnalysis(userSessionId) {
    const url = `${baseUrl}analysis/start`;
    const body = {
      userSessionId,
    };
    const response = await axios.post(url, body);
    return response.data;
  },
};
