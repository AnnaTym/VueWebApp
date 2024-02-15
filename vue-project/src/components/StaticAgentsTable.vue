<template>
    <div>
      <h1>Static Agents Table</h1>
      <table-lite
        :is-static-mode="true"
        :is-loading="table.isLoading"
        :columns="table.columns"
        :rows="table.rows"
        :total="table.totalRecordCount"
        :sortable="table.sortable"
        @is-finished="table.isLoading = false"
      ></table-lite>
    </div>
  </template>
  
<script>
  import TableLite from 'vue3-table-lite';
  export default {
      name: "Table",
      components: { TableLite },
      setup() {
        const table = {
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
          rows: [
            {
                "id": "3d887de3-8351-4391-b155-e174f472456a",
                "name": "string",
                "state": "",
                "skills": [
                    "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                    "3d887de3-8351-4391-b155-e174f472456a"
                ]
            },
            {
                "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "name": "anna",
                "state": "",
                "skills": [
                    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
                ]
            },
            {
                "id": "462f003a-362c-4bb2-8f40-056dac51fc26",
                "name": "jogn",
                "state": "ON_CALL",
                "skills": [
                    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
                ]
            },
            {
                "id": "db87580f-7533-475d-af58-89347ad642ba",
                "name": "jogn",
                "state": "ON_CALL",
                "skills": [
                    "ada9de71-0401-4141-b5d7-1ab5dc5bff76",
                    "c6cbff45-a97a-43d3-9d1c-a5c70edfee33"
                ]
            },
            {
                "id": "ca09b076-9475-4262-a058-c0c9011db740",
                "name": "anna test",
                "state": "ON_CALL",
                "skills": [
                    "8ce3827d-e286-4288-9d1a-78285e91f09a"
                ]
            }
          ],
          totalRecordCount: 5,
          sortable: {
            order: "id",
            sort: "asc",
          },
        };
    
        return {
          table,
        };
    },
  }
</script>