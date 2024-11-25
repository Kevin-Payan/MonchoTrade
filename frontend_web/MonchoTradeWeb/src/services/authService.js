import axios from 'axios';
import Cookies from 'js-cookie';
import { appsettings } from '../../settings/appsettings';
export default {
  state: {
    refreshInterval: null
  },

  startTokenRefresh() {
    this.refreshInterval = setInterval(async () => {
      try {
        await this.refreshAccessToken();
      } catch (error) {
        console.error('Error refreshing token:', error);
        this.stopTokenRefresh();
      }
    }, 4 * 60 * 1000); // 4 minutos
  },

  stopTokenRefresh() {
    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
      this.refreshInterval = null;
    }
  },

  async refreshAccessToken() {
    try {
      const response = await axios.post( `${appsettings.apiUrl}/session/refresh`, {}, {
        withCredentials: true // Enables sending cookies with request
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  }
  
};