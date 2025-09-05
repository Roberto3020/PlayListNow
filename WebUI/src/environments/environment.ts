export const environment = {
  production: false,
  apiUrl: window.location.hostname === 'localhost'
    ? 'http://localhost:8080/song'
    : 'http://webapi:8080/song'
};
