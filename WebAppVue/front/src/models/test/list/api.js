import api from "@/lib/api.js";

export default {
    gets() {
        return api({
            endpoint: "/test",
            method: "GET",
        });
    },
};
