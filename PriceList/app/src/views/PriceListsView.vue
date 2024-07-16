<template>
    <div>
        <v-card flat class="ml-2">
            <v-card-title class="subheading font-weight-bold pl-0">Прайс-листы</v-card-title>
        </v-card>

        <v-row>
            <v-col cols="12" sm="10" class="ml-2">
                <v-data-table :headers="headers"
                              :items="priceLists"
                              :items-per-page="10"
                              class="elevation-1"
                              :search="search"
                              :custom-filter="searchPriceList">
                    <template v-slot:top>
                        <v-text-field v-model="search"
                                      label="Поиск по названию"
                                      class="mx-4"></v-text-field>
                    </template>

                    <template v-slot:item.name="{ item }">
                        <router-link :to="{ name: 'priceListView', params: { id: item.id } }">
                            <span>{{ item.name }}</span>
                        </router-link>
                    </template>
                </v-data-table>
            </v-col>
        </v-row>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                search: "",
                headers: [
                    {
                        text: "Номер",
                        align: "center",
                        sortable: false,
                        value: "number",
                        width: "10%"
                    },
                    {
                        text: "Название",
                        align: "start",
                        sortable: true,
                        value: "name",
                        width: "60%"
                    },
                    {
                        text: "Дата",
                        value: "creationDate",
                        sortable: true,
                        width: "30%"
                    },
                ],
                priceLists: []
            }
        },

        created() {
            this.loadData();
        },

        methods: {
            loadData() {
                this.$store.dispatch("getPriceLists").then(priceLists => {
                    this.priceLists = priceLists.map((c, index) => {
                        return {
                            number: index + 1,
                            id: c.id,
                            name: c.name,
                            creationDate: this.formatDate(c.creationDate)
                        }
                    });
                });
            },

            formatDate(dateTime) {
                if (dateTime === null) {
                    return "-";
                }

                const date = new Date(Date.parse(dateTime));

                const options = {
                    year: "numeric",
                    month: "long",
                    day: "numeric"
                };

                return date.toLocaleString("ru", options);
            },

            searchPriceList(value, search, _) {
                return value != null &&
                    search != null &&
                    value.toString().trim().toLocaleLowerCase().indexOf(search.trim().toLocaleLowerCase()) !== -1;
            }
        }
    }
</script>
