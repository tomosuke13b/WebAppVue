import api from "@/lib/api.js";

export default {
    gets() {
        return api({
            endpoint: "/node",
            method: "GET",
        });
    },
    get(id) {
        return api({
            endpoint: "/node/" + id,
            method: "GET",
        });
    },
    post(body) {
        return api({
            endpoint: "/node",
            method: "POST",
            body
        });
    },
    put(id, body) {
        return api({
            endpoint: "/node/" + id,
            method: "PUT",
            body
        });
    },
    delete(id) {
        return api({
            endpoint: "/node/" + id,
            method: "DELETE",
        });
    },
    getImage(id) {
        return api({
            endpoint: "/image/" + id,
            method: "GET",
        });
    },
    postImage(image) {
        return api({
            endpoint: "/image",
            method: "POST",
            body: image
        });
    },
    putImage(id, image) {
        return api({
            endpoint: "/image/" + id,
            method: "PUT",
            body: image
        });
    },
};
