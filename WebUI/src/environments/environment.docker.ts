export const environment = {
  production: false,
  apiUrl: window.location.hostname === 'localhost'
    ? 'http://localhost:7173/song'
    : 'http://192.168.1.9:7173/song'
};
