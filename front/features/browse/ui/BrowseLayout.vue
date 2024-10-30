<script lang="ts" setup>

</script>

<template>
  <div class="flex gap-4 min-h-[0]">
    <div class="w-[30%]">
      <slot name="sidebar" />
    </div>
    <div class="grow flex flex-col justify-between gap-4  h-full max-h-full">
      <div class="h-[40%]">
        <slot name="top" />
      </div>
      <div class="grow">
        <slot name="bottom" />
      </div>
    </div>
  </div>
</template>