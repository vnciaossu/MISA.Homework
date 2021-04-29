import axios from "axios";
import NProgress from "nprogress";

// Add a request interceptor
axios.interceptors.request.use(
  function(config) {
    // Do something before request is sent
    NProgress.start();
    return config;
  },
  function(error) {
    // Do something with request error
    console.error(error);
    return Promise.reject(error);
  }
);

// Add a response interceptor
axios.interceptors.response.use(
  function(response) {
    // Do something with response data
    NProgress.done();
    return response;
  },
  function(error) {
    // Do something with response error
    console.error(error);
    return Promise.reject(error);
  }
);

export default axios;
