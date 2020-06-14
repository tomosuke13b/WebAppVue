import axios from "axios";

axios.defaults.baseURL = 'https://localhost:9443/api';
axios.defaults.headers.post['Content-Type'] = 'application/json;charset=utf-8';
axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';

export default {
    testApi(id, callback) {
        axios.get("/test/" + id)
            .then((res) => {
                callback(res.data);
            })
            .catch((error) => {
                console.log(error);
            });
    },
};