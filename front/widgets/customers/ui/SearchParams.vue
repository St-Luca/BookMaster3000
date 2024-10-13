<script lang="ts" setup>

</script>

<template>
  <UCard>
    <div class="grid grid-cols-[auto_1fr] gap-4 items-baseline">
      <div>ID</div>
      <UInput></UInput>
      <div>Имя</div>
      <UInput></UInput>
      <div></div>
      <UButton class="mt-2 justify-center">
        Поиск
        <template #leading>
          <UIcon name="i-heroicons-outline-magnifying-glass" class="w-3 h-3" />
        </template>
      </UButton>
    </div>
  </UCard>
</template>