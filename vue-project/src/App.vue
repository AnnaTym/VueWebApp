<template>
  <div id="app">
    <StaticAgentsTable/>
    <br/>
    <ApiAgentsTable />
</div>
</template>

<script>
  import StaticAgentsTable from './components/StaticAgentsTable.vue'
  import ApiAgentsTable from './components/ApiAgentsTable.vue';

  export default {
    name: 'App',
    components: {
      StaticAgentsTable,
      ApiAgentsTable
    }
  }
</script>