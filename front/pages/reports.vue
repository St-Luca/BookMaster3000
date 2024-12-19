<script lang="ts" setup>
import TabHistory from '~/widgets/reports/ui/TabHistory.vue';
import TabReminders from '~/widgets/reports/ui/TabReminders.vue';


definePageMeta({
  layout: 'full-height',
  middleware: 'auth',
})

const tabs = ref([
  {
    label: "Напоминания",
    slot: "reminders"
  },
  {
    label: "История книги",
    slot: "history"
  },
]);
</script>

<template>
  <UCard
    class="min-h-0 overflow-hidden"
    :ui="{
      body: {
        base: 'h-full overflow-hidden min-h-0',
      },
    }"
  >
    <h1 class="text-xl font-medium mb-3">Отчёты</h1>
    <UTabs
      :items="tabs"
      :ui="{
        list: {
          width: 'w-[600px]'
        }
      }"
    >
      <template #reminders>
        <TabReminders />
      </template>
      <template #history>
        <TabHistory />
      </template>
    </UTabs>
  </UCard>
</template>