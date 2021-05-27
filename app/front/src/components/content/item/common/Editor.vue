<template>
    <v-container>
        <v-row class="text-center">
            <v-col cols="12">
                <v-text-field
                    label="名称"
                    placeholder="name"
                    v-model="name"
                    :readonly="isView"
                    outlined
                ></v-text-field>
            </v-col>
            <v-col cols="12">
                <v-textarea
                    label="説明"
                    placeholder="description"
                    v-model="description"
                    :readonly="isView"
                    outlined
                ></v-textarea>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
export default {
    props: {
        isView: {
            type: Boolean,
            default: false
        },
        value: {
            type: Object,
            required: true,
        },
    },

    data() {
        return {
            name: "",
            description: "",
        };
    },
    computed: {
    },
    mounted() {
    },
    methods: {
    },
    watch: {
        value(value) {
            this.name = value.name;
            this.description = value.description;
        },
    },
    updated() {
        this.$emit("onEdit", {
            name: this.name,
            description: this.description,
        });
    },
}
</script>
