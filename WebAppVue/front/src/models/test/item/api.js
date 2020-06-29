import api from "@/lib/api.js";

export default {
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
};
