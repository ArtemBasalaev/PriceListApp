<template>
    <div>
        <div v-if="isLoading" class="text-center my-5">
            <v-progress-circular indeterminate
                                 color="primary"
                                 size="50"
                                 width="3"></v-progress-circular>
        </div>

        <template v-if="!isLoading">
            <v-card flat class="ml-2">
                <v-card-title class="subheading font-weight-bold pl-0">{{ priceListName }} от {{ priceListDate }}</v-card-title>
            </v-card>

            <v-row>
                <v-col cols="12" sm="10" class="ml-2">
                    <v-data-table :headers="headers"
                                  :items="priceListData"
                                  :items-per-page="10"
                                  class="elevation-1"
                                  :search="search"
                                  :custom-filter="searchPriceList">
                        <template v-slot:top>
                            <v-text-field v-model="search"
                                          label="Поиск по названию"
                                          class="mx-4"></v-text-field>

                            <v-dialog v-model="dialogDelete" max-width="250px">
                                <v-card>
                                    <v-card-title class="text-h5 pt-5">
                                        <v-spacer></v-spacer>
                                        Удалить товар?
                                        <v-spacer></v-spacer>
                                    </v-card-title>

                                    <v-card-actions class="py-5">
                                        <v-spacer></v-spacer>
                                        <v-btn color="primary" @click="dialogDelete = false">Отмена</v-btn>
                                        <v-btn color="error" @click="deleteProduct">Удалить</v-btn>
                                        <v-spacer></v-spacer>
                                    </v-card-actions>
                                </v-card>
                            </v-dialog>
                        </template>

                        <template v-slot:item.actions="{ item }">
                            <v-icon small
                                    @click="openDeleteDialog(item)">
                                mdi-delete
                            </v-icon>
                        </template>

                        <template v-slot:item.name="{ item }">
                            <router-link :to="{ name: 'priceListView', params: { id: item.id } }">
                                <span>{{ item.name }}</span>
                            </router-link>
                        </template>
                    </v-data-table>
                </v-col>
            </v-row>

            <v-row>
                <v-col>
                    <v-btn depressed
                           class="ml-2"
                           color="primary"
                           @click="dialog = true">
                        Добавить товар
                    </v-btn>
                </v-col>
            </v-row>

            <v-row justify="center">
                <v-dialog v-model="dialog"
                          persistent
                          max-width="600px">
                    <v-card>
                        <v-card-title>
                            <span class="text-h5">Характеристики товара</span>
                        </v-card-title>

                        <v-card-text>
                            <v-container>
                                <validation-observer ref="observer" v-slot="{ handleSubmit, errors }">
                                    <form @submit.prevent="handleSubmit(saveNewProduct)">
                                        <v-row>
                                            <v-col cols="12">
                                                <validation-provider tag="div"
                                                                     v-slot="{ errors }"
                                                                     rules="required">
                                                    <v-text-field v-model="newProduct.name"
                                                                  label="Название товара*"
                                                                  :error-messages="errors">
                                                    </v-text-field>
                                                </validation-provider>
                                            </v-col>

                                            <v-col cols="12">
                                                <validation-provider tag="div"
                                                                     v-slot="{ errors }"
                                                                     rules="required">
                                                    <v-text-field v-model="newProduct.code"
                                                                  label="Код товара"
                                                                  :error-messages="errors">
                                                    </v-text-field>
                                                </validation-provider>
                                            </v-col>
                                        </v-row>

                                        <template v-for="(column, i) in columns">
                                            <v-row align="center" :key="i">
                                                <v-col cols="12" sm="3" class="py-0">
                                                    <div class="text-body-1 font-weight-bold">{{ column.columnName.name }}</div>
                                                </v-col>

                                                <v-col cols="12" sm="9" class="py-0">
                                                    <validation-provider v-if="column.columnType.id === 1"
                                                                         tag="div"
                                                                         v-slot="{ errors }"
                                                                         rules="required">
                                                        <v-text-field v-model="newProduct.data[column.priceListColumnId]"
                                                                      label="Текст"
                                                                      :placeholder="column.columnType.name"
                                                                      :error-messages="errors">
                                                        </v-text-field>
                                                    </validation-provider>
                                                    <validation-provider v-else-if="column.columnType.id === 2"
                                                                         tag="div"
                                                                         v-slot="{ errors }"
                                                                         rules="required">
                                                        <v-textarea auto-grow
                                                                    outlined
                                                                    rows="3"
                                                                    v-model="newProduct.data[column.priceListColumnId]"
                                                                    row-height="30"></v-textarea>
                                                    </validation-provider>
                                                    <validation-provider v-else
                                                                         tag="div"
                                                                         v-slot="{ errors }"
                                                                         rules="required|numeric">
                                                        <v-text-field v-model.number="newProduct.data[column.priceListColumnId]"
                                                                      label="Число"
                                                                      :placeholder="column.columnType.name"
                                                                      :error-messages="errors">
                                                        </v-text-field>
                                                    </validation-provider>
                                                </v-col>
                                            </v-row>
                                        </template>

                                        <v-card-actions>
                                            <v-spacer></v-spacer>
                                            <v-col>
                                                <v-btn depressed
                                                       class="mr-2 mb-2"
                                                       color="error"
                                                       @click="closeDeleteDialog">
                                                    Закрыть
                                                </v-btn>

                                                <v-btn depressed
                                                       class="mb-2"
                                                       color="success"
                                                       @click="dialog = false"
                                                       :disabled ="Object.entries(errors).some(err => err[1].length !== 0)"
                                                       type="submit">
                                                    Сохранить
                                                </v-btn>
                                            </v-col>
                                        </v-card-actions>
                                    </form>
                                </validation-observer>
                            </v-container>
                            <small>* Поля необходимые для заполнения</small>
                        </v-card-text>
                    </v-card>
                </v-dialog>
            </v-row>
        </template>
    </div>
</template>

<script>
    import _ from "lodash";
    import Utils from "@/common/utils";

    export default {
        props: {
            id: {
                required: true
            }
        },

        data() {
            return {
                dialog: false,
                isLoading: true,
                dialogDelete: false,
                search: "",
                headers: [],
                priceListData: [],
                priceListName: "",
                priceListDate: null,
                columns: [],
                dataToDelete: {},
                newProduct: {
                    name: "",
                    code: "",
                    data: {}
                }
            }
        },

        created() {
            this.loadData();
        },

        methods: {
            loadData() {
                this.isLoading = true;

                this.$store.dispatch("getPriceList", this.id).then(({ columns, priceList, productsPriceListData }) => {
                    this.columns = columns;
                    this.priceListName = priceList.name;
                    this.priceListDate = this.formatDate(priceList.creationDate);

                    const headers = columns.map((c, index) => {
                        return {
                            text: c.columnName.name,
                            align: "start",
                            value: `p.${index + 1}_` + c.priceListColumnId
                        };
                    });

                    this.headers = [
                        {
                            text: "Номер",
                            value: "number",
                        },
                        {
                            text: "Название",
                            align: "start",
                            value: "productName",
                        },
                        {
                            text: "Код",
                            align: "start",
                            value: "productCode",
                        },
                        ...headers,
                        {
                            text: "Удалить",
                            value: "actions",
                            sortable: false
                        }
                    ];

                    if (productsPriceListData && productsPriceListData.length != 0) {
                        this.priceListData = productsPriceListData.map((p, index) => {
                            const product = {
                                number: index + 1,
                                productName: p.product.name,
                                product: p.product.code
                            }

                            const data = {
                                recordIds: []
                            };

                            let i = 1;

                            for (const columnData of p.columnsData) {
                                data.recordIds.push(columnData.id);
                                data[`p.${i}_` + columnData.priceListColumnId] = columnData.value;
                                i++;
                            }

                            return { ...product, ...data };
                        });
                    }

                    this.isLoading = false;
                })
            },

            formatDate: Utils.formatDate,

            searchPriceList(value, search, _) {
                return value != null &&
                    search != null &&
                    value.toString().trim().toLocaleLowerCase().indexOf(search.trim().toLocaleLowerCase()) !== -1;
            },

            closeDeleteDialog() {
                this.dialog = false;

                this.newProduct = {
                    name: "",
                    code: "",
                    data: {}
                };

                this.$refs.observer.reset();
            },

            openDeleteDialog(item) {
                this.dataToDelete = item;
                this.dialogDelete = true;
            },

            deleteProduct() {
                this.dialogDelete = false;

                const index = this.priceListData.indexOf(this.dataToDelete);
                const recordIds = this.priceListData[index].recordIds;

                this.$store.dispatch("deleteProduct", recordIds).then(_ => {
                    this.priceListData.splice(index, 1);
                    this.dataToDelete = null;
                    this.$store.commit("toast/success", "Товар успешно удален");
                });
            },

            saveNewProduct() {
                this.isLoading = true;

                const newProductRequest = {
                    name: this.newProduct.name,
                    code: this.newProduct.code
                };

                this.$store.dispatch("addNewProduct", newProductRequest).then(product => {
                    const columnsData = [];

                    for (const [priceListColumnId, value] of Object.entries(this.newProduct.data)) {
                        columnsData.push({
                            priceListColumnId,
                            value
                        });
                    }

                    const addProductDataToPriceListRequest = {
                        productId: product.id,
                        columnsData
                    };

                    this.$store.dispatch("addDataInPriceList", addProductDataToPriceListRequest).then(_ => {
                        this.newProduct = {};
                        this.loadData();
                        this.isLoading = false;

                    })
                })
            }
        }
    };
</script>
