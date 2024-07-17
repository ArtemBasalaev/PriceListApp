<template>
    <div>
        <v-card flat>
            <v-card-title class="subheading font-weight-bold pl-0">Добавление прайс-лист</v-card-title>
        </v-card>

        <div>
            <validation-observer ref="observer" v-slot="{ handleSubmit }">
                <form @submit.prevent="handleSubmit(savePricetList)">
                    <v-row>
                        <v-col cols="10">
                            <validation-provider tag="div"
                                                 v-slot="{ errors }"
                                                 rules="required">
                                <v-text-field v-model="priceListName"
                                              label="Название прайс-листа"
                                              :error-messages="errors">
                                </v-text-field>
                            </validation-provider>
                        </v-col>
                    </v-row>

                    <template v-for="(column, i) in priceListColumns">
                        <v-row align="center" :key="i">
                            <v-col cols="11" sm="6">
                                <validation-provider tag="div"
                                                     v-slot="{ errors }"
                                                     rules="required">
                                    <v-combobox :items="existingColumnNames"
                                                v-model="column.name"
                                                item-text="name"
                                                item-value="id"
                                                :error-messages="errors"
                                                label="Название колонки">
                                    </v-combobox>
                                </validation-provider>
                            </v-col>

                            <v-col cols="11" sm="3">
                                <validation-provider tag="div"
                                                     v-slot="{ errors }"
                                                     rules="required">
                                    <v-select :items="columnsTypes"
                                              item-text="name"
                                              item-value="id"
                                              v-model="column.type.id"
                                              :error-messages="errors"
                                              label="Тип колонки">
                                    </v-select>
                                </validation-provider>
                            </v-col>

                            <v-col cols="auto">
                                <v-btn depressed
                                       color="error"
                                       @click="removeColumn(i)">
                                    Удалить
                                </v-btn>
                            </v-col>
                        </v-row>
                    </template>

                    <v-row>
                        <v-col>
                            <v-btn depressed
                                   class="mr-2 mb-2"
                                   color="primary"
                                   @click="addColumn">
                                Добавить колонку
                            </v-btn>
                            <v-btn depressed
                                   class="mb-2"
                                   color="success"
                                   type="submit">
                                Сохранить прайс-лист
                            </v-btn>
                        </v-col>
                    </v-row>
                </form>
            </validation-observer>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                priceListName: "",
                priceListColumns: [],
                existingColumnNames: [],
                columnsTypes: []
            };
        },

        created() {
            this.loadData();
        },

        methods: {
            addColumn() {
                this.priceListColumns.push({
                    name: null,
                    type: {
                        id: null,
                        name: ""
                    }
                });
            },

            removeColumn(index) {
                this.priceListColumns.splice(index, 1);
            },

            loadData() {
                Promise.all([
                    this.$store.dispatch("getColumnNames").then(existingColumnNames => this.existingColumnNames = existingColumnNames),
                    this.$store.dispatch("getColumnTypes").then(columnsTypes => this.columnsTypes = columnsTypes)
                ]);
            },

            savePricetList() {
                const createPriceListRequest = {
                    name: this.priceListName,
                    columns: this.priceListColumns.map(c => {
                        if (typeof c.name === "string") {
                            c.name = {
                                id: 0,
                                name: c.name
                            };
                        }

                        return {
                            columnName: c.name,
                            columnType: c.type
                        };
                    })
                }

                this.$store.dispatch("createPriceList", createPriceListRequest).then(_ => {
                    //this.$store.state.connection.invoke("NotifyPriceListCreated", this.priceListName, "admin")
                    
                    this.priceListName = "";
                    this.priceListColumns = [];
                    this.$refs.observer.reset();
                });
            }
        }
    }
</script>
