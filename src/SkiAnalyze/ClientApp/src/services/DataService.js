import axios from 'axios';

const baseUrl = 'https://localhost:5001/api/';

export default {
  async getGondolas(bounds) {
    let url = `${baseUrl}gondolas`;
    url += `?SwLat=${bounds.southWest.latitude}&SwLon=${bounds.southWest.longitude}`;
    url += `&NeLat=${bounds.northEast.latitude}&NeLon=${bounds.northEast.longitude}`;
    const response = await axios.get(url);
    return response.data;
  },
  async getGondola(id) {
    const url = `${baseUrl}gondolas/${id}`;
    const response = await axios.get(url);
    return response.data;
  },
  async getPistes(bounds) {
    let url = `${baseUrl}pistes`;
    url += `?SwLat=${bounds.southWest.latitude}&SwLon=${bounds.southWest.longitude}`;
    url += `&NeLat=${bounds.northEast.latitude}&NeLon=${bounds.northEast.longitude}`;
    const response = await axios.get(url);
    return response.data;
  },
  async getTracks() {
    const url = `${baseUrl}tracks`;
    const response = await axios.get(url);
    return response.data;
  },
  async getTrack(id) {
    const url = `${baseUrl}tracks/${id}`;
    const response = await axios.get(url);
    return response.data;
  },
  async createTrack(track) {
    const formData = new FormData();
    formData.append('file', track.file);
    formData.append('name', track.name);
    formData.append('color', track.color);
    formData.append('fileType', track.fileType);

    const url = `${baseUrl}tracks`;
    const response = await axios.post(url, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },
  async removeTrack(trackId) {
    const url = `${baseUrl}tracks/${trackId}`;
    await axios.delete(url);
  },
  async startAnalysis(trackId) {
    const url = `${baseUrl}tracks/${trackId}/analysis/start`;
    const response = await axios.post(url);
    return response.data;
  },
  async getAnalysisStatus(trackId) {
    const url = `${baseUrl}tracks/${trackId}/analysis/status`;
    const response = await axios.get(url);
    return response.data;
  },
  async getPreview(trackId) {
    const url = `${baseUrl}tracks/${trackId}/analysis/preview`;
    const response = await axios.get(url);
    return response.data;
  },
  async getAnalysisResult(trackId) {
    const url = `${baseUrl}tracks/${trackId}/analysis/result`;
    const response = await axios.get(url);
    return response.data;
  },
  async getDifficultyStats(trackId) {
    const url = `${baseUrl}tracks/${trackId}/stats/difficulty`;
    const response = await axios.get(url);
    return response.data;
  },
  async getGondolaCountByPropertyStats(trackId, propertyName) {
    const url = `${baseUrl}tracks/${trackId}/stats/gondolacount?propertyName=${propertyName}`;
    const response = await axios.get(url);
    return response.data;
  },
  async getHeartRatesPerPisteDifficulty(trackId) {
    const url = `${baseUrl}tracks/${trackId}/stats/heartRatesPerDiff`;
    const response = await axios.get(url);
    return response.data;
  },
  async getSkiAreas(bounds) {
    let url = `${baseUrl}skiareas`;
    url += `?SwLat=${bounds.southWest.latitude}&SwLon=${bounds.southWest.longitude}`;
    url += `&NeLat=${bounds.northEast.latitude}&NeLon=${bounds.northEast.longitude}`;
    const response = await axios.get(url);
    return response.data;
  },
  async getSkiArea(id) {
    const url = `${baseUrl}skiareas/${id}`;
    const response = await axios.get(url);
    return response.data;
  },
  async getTotals() {
    const url = `${baseUrl}stats/totals`;
    const response = await axios.get(url);
    return response.data;
  },
  async getTimeline(propertyName, dateRange) {
    let url = `${baseUrl}stats/timeline?byProperty=${propertyName}`;
    if (dateRange && dateRange.length === 2) {
      url += `&startDate=${dateRange[0].toISOString()}&endDate=${dateRange[1].toISOString()}`;
    }
    const response = await axios.get(url);
    return response.data;
  },
};
