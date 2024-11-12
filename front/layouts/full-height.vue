<script lang="ts" setup>
import AppHeader from '~/widgets/general-layout/ui/AppHeader.vue';
</script>

<template>
  <div class="md:h-[100vh] md:max-h-[100vh] flex flex-col gap-4 p-4">
    <AppHeader></AppHeader>
    <NuxtPage class="grow"></NuxtPage>
  </div>
</template>