import axios from 'axios';

const baseUrl = 'https://localhost:5001/';

export default {
  async getGondolas() {
    const response = await axios.get(`${baseUrl}gondolas?nwLat=46.982524&nwLon=10.888466&seLat=46.914135&seLon=11.036374`);
    return response.data;
  },
  async getPistes() {
    const response = await axios.get(`${baseUrl}pistes?nwLat=46.982524&nwLon=10.888466&seLat=46.914135&seLon=11.036374`);
    return response.data;
  },
};
