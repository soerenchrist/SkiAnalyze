// import L from 'leaflet';
import L from 'leaflet-hotline';

const getPropertyValue = (trackPoint, property) => {
  if (property in trackPoint) return trackPoint[property];
  return 0;
};

export default (runs, property) => {
  const lines = [];
  let min = 10000;
  let max = -10000;

  runs.forEach((run) => {
    run.coordinates.forEach((point) => {
      const value = getPropertyValue(point, property);
      if (value < min) min = value;
      if (value > max) max = value;
      lines.push([point.latitude, point.longitude, value]);
    });
  });

  const hotline = L.hotline(lines, {
    min,
    max,
    palette: {
      0.0: '#008800',
      0.5: '#ffff00',
      1.0: '#ff0000',
    },
    weight: 5,
    outlineColor: '#000000',
    outlineWidth: 1,
  });

  return hotline;
};
