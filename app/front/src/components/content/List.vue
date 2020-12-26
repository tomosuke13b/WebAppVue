<template>
    <v-container>
        <v-btn class="mx-2" fixed right fab dark style="bottom: 80px" color="indigo" @click="newItem">
            <v-icon dark>mdi-plus</v-icon>
        </v-btn>
        <v-row class="text-center">
            <v-col
                v-for="(item, index) in items"
                :key="index">
                <ItemCard :item="item" @onEdit="onEdit" @onView="onView" style="padding-bottom: 2px" />
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import { ItemCard } from "./card";

export default {
    components: { ItemCard },

    props: {
    },
    data: () => ({
    }),
    computed: {
        items() {
            return this.$store.getters["list/items"];
        }
    },
    mounted() {
        this.$store.dispatch("list/load");
    },
    methods: {
        onEdit(id) {
            if(id === undefined || id == -1) return;
            this.$router.push({ name: "detail", params: { id: id, isView: false } });
        },
        onView(id) {
            if(id === undefined || id == -1) return;
            this.$router.push({ name: "detail", params: { id: id, isView: true } });
        },
        newItem() {
            this.$router.push( { name: "new" } );
        }
    }
}
</script>
