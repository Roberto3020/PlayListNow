export const environment = {
  production: false,
  apiUrl: window.location.hostname === 'localhost'
    ? 'http://localhost:8080/song'
    : 'http://192.168.1.13:8080/song'
};
