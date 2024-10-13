<script lang="ts" setup>

const items = ref([
  {
    label: "Книги",
    to: "/browse-books"
  },
  {
    label: "Клиенты",
    dropdown: [[
      {
        label: "Найти",
        to: "/browse-customers",
        class: "bg-white hover:bg-gray-50"
      },
      {
        label: "Добавить",
        to: "/browse-customers",
        class: "bg-white hover:bg-gray-50"
      },
    ]]
  }
])

</script>

<template>
  <header>
    <UCard
      :ui="{
        body: {
          padding: '!py-2',
        },
      }"
    >
      <template #default>
        <nav class="flex gap-2">
          <template v-for="item in items">
            <UDropdown
              v-if="item.dropdown"
              :items="item.dropdown"
            >
              <UButton
                :variant="'ghost'"
                :label="item.label"
                color="black"
                class="hover:bg-gray-100"
              />
            </UDropdown>
            <UButton
              v-else
              :variant="'ghost'"
              :label="item.label"
              :to="item.to"
              color="black"
              class="hover:bg-gray-100"
            />
          </template>
        </nav>
      </template>
    </UCard>
  </header>
</template>