import promiseLimit from "promise-limit";

const limit = promiseLimit(6);

export default {
    Tasks: [],
    init() {
        this.Tasks = [];
    },
    register(task) {
        this.Tasks.push(async () => {
            await task();
        });
    },
    exec() {
        if (!this.Tasks) return;
        return Promise.all(
            this.Tasks.map(task => {
                return limit(() => task());
            })
        );
    },
}
