<script lang="ts" setup>
import type { Book } from '~/entities/book';
import BookCard from '~/widgets/browse-layout/ui/BookCard.vue';
import BooksList from '~/widgets/browse-layout/ui/BooksList.vue';
import InputFields from '~/widgets/browse-layout/ui/SearchParams.vue';

const mockupList = ref<Book[]>([
  {
    key: "1",
    title: "Преступление и наказание",
    subtitle: "Роман в шести частях с эпилогом",
    firstPublishDate: "1866",
    description: "История студента Родионa Раскольникова, его преступления и искупления.",
    authors: [
      {
        key: "a1",
        name: "Фёдор Достоевский",
        birthDate: "1821-11-11",
        deathDate: "1881-02-09",
      },
    ],
    subjects: [ "Классика", "Философия", "Психология" ],
  },
  {
    key: "2",
    title: "Война и мир",
    subtitle: "Роман-эпопея",
    firstPublishDate: "1869",
    description: "Эпическая история о жизни российского дворянства во время Наполеоновских войн.",
    authors: [
      {
        key: "a2",
        name: "Лев Толстой",
        birthDate: "1828-09-09",
        deathDate: "1910-11-20",
      },
    ],
    subjects: [ "Классика", "История" ],
  },
  {
    key: "3",
    title: "Мастер и Маргарита",
    subtitle: "Роман",
    firstPublishDate: "1967",
    description: "История о любви, мистике и сатире в СССР 1930-х годов.",
    authors: [
      {
        key: "a3",
        name: "Михаил Булгаков",
        birthDate: "1891-05-15",
        deathDate: "1940-03-10",
      },
    ],
    subjects: [ "Мистика" ],
  },
  {
    key: "4",
    title: "Евгений Онегин",
    subtitle: "Роман в стихах",
    firstPublishDate: "1833",
    description: "История любви, дружбы и трагедии молодого дворянина Евгения Онегина.",
    authors: [
      {
        key: "a4",
        name: "Александр Пушкин",
        birthDate: "1799-06-06",
        deathDate: "1837-01-29",
      },
    ],
    subjects: [ "Поэзия", "Драма" ],
  },
  {
    key: "5",
    title: "Анна Каренина",
    subtitle: "Роман",
    firstPublishDate: "1877",
    description: "Трагическая история любви Анны Карениной и её падения в аристократическом обществе.",
    authors: [
      {
        key: "a2",
        name: "Лев Толстой",
        birthDate: "1828-09-09",
        deathDate: "1910-11-20",
      },
    ],
    subjects: [ "Классика", "Драма" ],
  },
  {
    key: "6",
    title: "Мёртвые души",
    subtitle: "Поэма",
    firstPublishDate: "1842",
    description: "История мошенника Чичикова, покупающего умерших крестьян для обогащения.",
    authors: [
      {
        key: "a5",
        name: "Николай Гоголь",
        birthDate: "1809-04-01",
        deathDate: "1852-03-04",
      },
    ],
    subjects: [ "Драма" ],
  },
  {
    key: "7",
    title: "Отцы и дети",
    subtitle: "Роман",
    firstPublishDate: "1862",
    description: "История столкновения поколений и разницы взглядов на жизнь в Российской империи.",
    authors: [
      {
        key: "a6",
        name: "Иван Тургенев",
        birthDate: "1818-10-28",
        deathDate: "1883-09-03",
      },
    ],
    subjects: [ "Драма" ],
  },
  {
    key: "8",
    title: "Доктор Живаго",
    subtitle: "Роман",
    firstPublishDate: "1957",
    description: "История врача Юрия Живаго на фоне революционных событий в России.",
    authors: [
      {
        key: "a7",
        name: "Борис Пастернак",
        birthDate: "1890-02-10",
        deathDate: "1960-05-30",
      },
    ],
    subjects: [ "История" ],
  },
]);

const viewedBook = ref<Book|undefined>(undefined);

const handleBookSelect = (book: Book) => {
  viewedBook.value = book;
}

</script>

<template>
  <div class="flex gap-4 h-[100vh] p-4">
    <InputFields class="w-[30%]"></InputFields>
    <div class="grow flex flex-col gap-4">
      <BooksList
        :list="mockupList"
        class="h-[40%]"
        :viewedBook="viewedBook"
        @bookSelect="handleBookSelect"
      />
      <BookCard
        :book="viewedBook"
        class="grow"
        :key="viewedBook?.key"
      />
    </div>
  </div>
</template>