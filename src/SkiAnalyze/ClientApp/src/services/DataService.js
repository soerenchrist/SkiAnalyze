import axios from 'axios';

const baseUrl = 'https://localhost:5001/api/';
const client = axios.create({
  baseURL: baseUrl,
  validateStatus: () => true,
});

export default {
  async getGondolas(bounds) {
    let url = 'gondolas';
    url += `?SwLat=${bounds.southWest.latitude}&SwLon=${bounds.southWest.longitude}`;
    url += `&NeLat=${bounds.northEast.latitude}&NeLon=${bounds.northEast.longitude}`;
    const response = await client.get(url);
    return response.data;
  },
  async getGondola(id) {
    const url = `gondolas/${id}`;
    const response = await client.get(url);
    return response.data;
  },
  async getPistes(bounds) {
    let url = 'pistes';
    url += `?SwLat=${bounds.southWest.latitude}&SwLon=${bounds.southWest.longitude}`;
    url += `&NeLat=${bounds.northEast.latitude}&NeLon=${bounds.northEast.longitude}`;
    const response = await client.get(url);
    return response.data;
  },
  async getTracks() {
    const url = 'tracks';
    const response = await client.get(url);
    return response.data;
  },
  async getTrack(id) {
    const url = `tracks/${id}`;
    const response = await client.get(url);
    return response.data;
  },
  async createTrack(track) {
    const formData = new FormData();
    formData.append('file', track.file);
    formData.append('name', track.name);
    formData.append('color', track.color);
    formData.append('fileType', track.fileType);

    const url = 'tracks';
    const response = await client.post(url, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },
  async removeTrack(trackId) {
    const url = `tracks/${trackId}`;
    await client.delete(url);
  },
  async startAnalysis(trackId) {
    const url = `tracks/${trackId}/analysis/start`;
    const response = await client.post(url);
    return response.data;
  },
  async getAnalysisStatus(trackId) {
    const url = `tracks/${trackId}/analysis/status`;
    const response = await client.get(url);
    return response.data;
  },
  async getPreview(trackId) {
    const url = `tracks/${trackId}/analysis/preview`;
    const response = await client.get(url);
    return response.data;
  },
  async getAnalysisResult(trackId) {
    const url = `tracks/${trackId}/analysis/result`;
    const response = await client.get(url);
    return response.data;
  },
  async getDifficultyStats(trackId) {
    const url = `tracks/${trackId}/stats/difficulty`;
    const response = await client.get(url);
    return response.data;
  },
  async getGondolaCountByPropertyStats(trackId, propertyName) {
    const url = `tracks/${trackId}/stats/gondolacount?propertyName=${propertyName}`;
    const response = await client.get(url);
    return response.data;
  },
  async getHeartRatesPerPisteDifficulty(trackId) {
    const url = `tracks/${trackId}/stats/heartRatesPerDiff`;
    const response = await client.get(url);
    return response.data;
  },
  async getSkiAreas(bounds) {
    let url = 'skiareas';
    url += `?SwLat=${bounds.southWest.latitude}&SwLon=${bounds.southWest.longitude}`;
    url += `&NeLat=${bounds.northEast.latitude}&NeLon=${bounds.northEast.longitude}`;
    const response = await client.get(url);
    return response.data;
  },
  async getSkiArea(id) {
    const url = `skiareas/${id}`;
    const response = await client.get(url);
    return response.data;
  },
  async getGondolasForSkiArea(id) {
    const url = `skiareas/${id}/gondolas`;
    const response = await client.get(url);
    return response.data;
  },
  async addGondolaToRuns(trackId, gondolaId, position) {
    const url = 'tracks/addGondola';
    const payload = {
      trackId,
      gondolaId,
      position,
    };
    await client.post(url, payload);
  },
  async getPistesForSkiArea(id) {
    const url = `skiareas/${id}/pistes`;
    const response = await client.get(url);
    return response.data;
  },
  async getTotals() {
    const url = 'stats/totals';
    const response = await client.get(url);
    return response.data;
  },
  async getTimeline(propertyName, dateRange) {
    let url = `stats/timeline?byProperty=${propertyName}`;
    if (dateRange && dateRange.length === 2) {
      url += `&startDate=${dateRange[0].toISOString()}&endDate=${dateRange[1].toISOString()}`;
    }
    const response = await client.get(url);
    return response.data;
  },
  async removeRun(trackId, runId) {
    const url = `tracks/${trackId}/runs/${runId}`;
    await client.delete(url);
  },
};
