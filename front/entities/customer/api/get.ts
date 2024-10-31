import { api } from "~/shared/util";
import type { Customer } from "../types";

export const getCustomer = (id:string) : Promise<Customer|undefined> => {
  // return api<Customer>(`/Clients/${id}`, {
  //   method: "GET"
  // }).then(res => {
  //   if (!res._data) return undefined;
  //   if (res.statusText !== "OK") return undefined;
  //   return res._data;
  // })
  return new Promise((resolve) => {
    resolve(mockCustomers.find(customer => customer.id === id));
  })
}

const mockCustomers:Customer[] = [
  {
    id: "1",
    name: "Иванов Иван Иванович",
    phone: "+79000000000",
    email: "ivanov@ivanov.com",
    city: "Москва",
    address: "ул. Пушкина, д. 1",
    zip: "123456",
    hasBooks: [
      {
        book: {
          id: "1",
          title: "Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
      {
        book: {
          id: "2",
          title: "Не Война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
      },
    ],
    circulationHistory: [
      {
        book: {
          id: "3",
          title: "Точно не война и мир",
          subtitle: "",
          authors: [],
          subjects: [],
          description: "Описание книги",
          firstPublishDate: "1869-07-26",
          coverImg: "https://picsum.photos/200"
        },
        key: "1",
        issueDate: new Date('2024-08-30'),
        returnDatePlanned: new Date('2024-11-21'),
        returnDateActual: new Date('2024-11-21'),
      },
    ]
  },
]