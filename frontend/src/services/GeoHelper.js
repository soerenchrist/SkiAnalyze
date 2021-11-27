// eslint-disable-next-line import/prefer-default-export
export default {
  getBounds: (coordinates) => {
    let minLat = 10000;
    let minLon = 10000;
    let maxLat = -10000;
    let maxLon = -10000;
    coordinates.forEach((x) => {
      if (x.latitude < minLat) minLat = x.latitude;
      if (x.longitude < minLon) minLon = x.longitude;
      if (x.latitude > maxLat) maxLat = x.latitude;
      if (x.longitude > maxLon) maxLon = x.longitude;
    });

    return {
      southWest: {
        latitude: minLat,
        longitude: minLon,
      },
      northEast: {
        latitude: maxLat,
        longitude: maxLon,
      },
    };
  },
};
