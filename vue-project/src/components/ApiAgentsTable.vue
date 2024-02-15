<template>
    <div>
    <h1>API Agents Table</h1>
    <table-lite
      :is-loading="table.isLoading"
      :columns="table.columns"
      :rows="table.rows"
      :total="table.totalRecordCount"
      :sortable="table.sortable"
      @do-search="doSearch"
      @is-finished="tableLoadingFinish"
    >
    <template v-slot:name="data">
      {{ data.value.name }}
    </template>
</table-lite>
</div>
</template>

<script>
  import TableLite from 'vue3-table-lite';
  import axios from 'axios';
  import {defineComponent, reactive} from 'vue'
  export default defineComponent({
    name: "ApiTable",
    components: { TableLite },
    setup() {
      const table = reactive({
        isLoading: false,
        columns: [
          {
            label: 'ID',
            field: 'id',
            sortable: true,
          },
          {
            label: 'Name',
            field: 'name',
            sortable: true,
          },
          {
            label: 'State',
            field: 'state',
            sortable: true,
          },
          {
            label: 'Skills',
            field: 'skills',
            sortable: true,
          },
        ],
        rows: [],
        totalRecordCount: 0,
        sortable: {
          order: "id",
          sort: "asc",
        },
      });

       /**
     * Table search event
     */
       const doSearch = (order, sort) => {
       table.isLoading = true;

      // Start use axios to get data from Server
      let url = 'https://localhost:7217/api/callcenter';
      axios.get(url)
      .then((response) => {
        // refresh table rows
        table.rows = response.data;
        table.totalRecordCount = response.data.length;
        table.sortable.order = order;
        table.sortable.sort = sort;
      });
      // End use axios to get data from Server
       };

      /**
       * Table search finished event
       */
      const tableLoadingFinish = (elements) => {
        table.isLoading = false;
      };

      // Get data first
      doSearch('id', 'asc');

      return {
        table,
        doSearch,
        tableLoadingFinish,
      };
    },
  })
</script>