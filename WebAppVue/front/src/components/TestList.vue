<template>
    <v-container>
        <v-btn class="mx-2" fixed right fab dark style="bottom: 80px" color="indigo" to="/test/new">
            <v-icon dark>mdi-plus</v-icon>
        </v-btn>
        <v-row
            class="text-center"
            v-for="(item, index) in items"
            :key="index">
            <v-col>
                <ItemCard :item="item" @onEdit="onEdit" @onView="onView" style="padding-bottom: 2px" />
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
    import { ItemCard } from "@/components/card";

    export default {
        name: "TestList",
        components: { ItemCard },

        props: {
        },
        data: () => ({
        }),
        computed: {
            items() {
                return this.$store.getters["list/getItems"];
            },
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
