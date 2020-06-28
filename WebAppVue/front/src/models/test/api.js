import api from "@/lib/api.js";

export default {
    gets() {
        return api({
            endpoint: "/test",
            method: "GET",
        });
    },
    get(id) {
        return api({
            endpoint: "/test/" + id,
            method: "GET",
        });
    },
    post(body) {
        return api({
            endpoint: "/test",
            method: "POST",
            body
        });
    },
    put(id, body) {
        return api({
            endpoint: "/test/" + id,
            method: "PUT",
            body
        });
    },
    delete(id) {
        return api({
            endpoint: "/test/" + id,
            method: "DELETE",
        });
    },
    // testApi(callback) {
    //     axios.get("/test")
    //         .then((res) => {
    //             callback(res.data);
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // },
};
