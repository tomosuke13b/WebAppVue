import axios from "axios";
import cookie from "js-cookie";

axios.defaults.baseURL = 'https://localhost:9443/api';

const api = axios.create({
    headers: {
        "X-CSRF-Token": cookie.get("XSRF-TOKEN") || "",
        "Content-Type": "application/json; charset=utf-8",
        "Access-Control-Allow-Origin": "*"
    },
    withCredentials: true,
});

export default ({
    method = "GET",
    endpoint,
    // callback = null,
    body = {},
    exceptionHandler = function (ex) {
        console.error(ex);
        throw ex;
    }
}) => {
    const responseHandler = async response => {
        const contentType = response.headers["content-type"];
        if (!response.status == 200) {
            // ERROR
            throw response;
        }

        // if (callback != null) callback();
        if (contentType && contentType.includes("application/json")) {
            return response.data;
        } else {
            return response;
        }
    };

    return api({
        method,
        data: body,
        url: endpoint
    })
        .then(responseHandler)
        .catch(exceptionHandler);
};
