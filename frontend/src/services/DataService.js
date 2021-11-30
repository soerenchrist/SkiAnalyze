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
  async createTrack(track) {
    const url = `${baseUrl}tracks`;
    const response = await axios.post(url, track);
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
};
