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
        console.log(body);
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
    getImage(id) {
        return api({
            endpoint: "/image/" + id,
            method: "GET",
        });
    },
    postImage(image) {
        console.log(image);
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
