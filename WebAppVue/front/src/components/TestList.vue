<template>
    <v-container>
        <v-row class="text-center">
            <v-col cols="12">
                <v-col class="mb-4">
                    <h1 class="display-2 font-weight-bold mb-3">
                        一覧
                    </h1>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/">Home</v-btn>
                </v-col>
                <v-col>
                    <v-btn small color="primary" to="/test/new">New</v-btn>
                </v-col>
                <v-col>
                    <v-list>
                        <v-list-item
                            v-for="(item, index) in items"
                            :key="index"
                        >
                            <v-list-item-content>
                                <v-list-item-title v-text="item.index"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-content>
                                <v-list-item-title v-text="item.name"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-content>
                                <v-list-item-title v-text="item.timeStamp"></v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-action>
                                <v-btn small color="primary" @click="onEdit(item.id)"  >編集</v-btn>
                            </v-list-item-action>
                            <v-list-item-action>
                                <v-btn small color="primary" @click="onView(item.id)" >参照</v-btn>
                            </v-list-item-action>

                        </v-list-item>
                    </v-list>
                </v-col>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    export default {
        name: "TestList",

        props: {
        },
        data: () => ({
        }),
        computed: {
            items() {
                return this.$store.getters["list/getItems"];
            }
        },
        mounted() {
            this.$store.dispatch("list/load");
        },
        methods: {
            onEdit(id) {
                if(id === undefined || id == -1) return;
                this.$router.push({ name: "testDetail", params: { id: id, isView: false } });
            },
            onView(id) {
                if(id === undefined || id == -1) return;
                this.$router.push({ name: "testDetail", params: { id: id, isView: true } });
            }
        }
    }
</script>
