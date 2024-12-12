import type { Book } from "../book";

export interface Exhibition {
  id: string|number;
  name: string;
  description: string;
  dateCreated: string;
  books: Book[];
}

export interface CreatedExhibition {
  name: string;
  description: string;
}

export interface ExhibitionsListResponse {
  itemsCount: number
  page: number;
  pageLimit: number;
  pages: number;
  exhibitions: Array<Exhibition>
}

export const exhibitionsListSample: ExhibitionsListResponse = {
  itemsCount: 100,
  page: 1,
  pageLimit: 20,
  pages: 5,
  exhibitions: [
    {
      id: 1,
      name: "International Book Fair",
      description: "The annual international book fair at the University of Helsinki",
      dateCreated: "2022-01-01",
      books: [
        {
          id: 1,
          title: "The Catcher in the Rye",
          subtitle: "J.D. Salinger",
          description: "A classic novel by Harper Lee",
          firstPublishDate: "1951-07-11",
          coverImg: "https://example.com/catcher-in-the-rye.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 1,
              name: "J.D. Salinger",
            },
          ],
        },
        {
          id: 2,
          title: "To Kill a Mockingbird",
          subtitle: "Harper Lee",
          description: "A classic novel by Harper Lee",
          firstPublishDate: "1960-07-11",
          coverImg: "https://example.com/to-kill-a-mockingbird.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 1,
              name: "Harper Lee",
            },
          ],
        },
        {
          id: 3,
          title: "1984",
          subtitle: "George Orwell",
          description: "A dystopian novel by George Orwell",
          firstPublishDate: "1949-06-01",
          coverImg: "https://example.com/1984.jpg",
          subjects: ["Fiction", "Dystopian"],
          authors: [
            {
              id: 2,
              name: "George Orwell",
            },
            ],
        },
        {
          id: 4,
          title: "The Great Gatsby",
          subtitle: "F. Scott Fitzgerald",
          description: "A classic novel by F. Scott Fitzgerald",
          firstPublishDate: "1925-02-19",
          coverImg: "https://example.com/the-great-gatsby.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 3,
              name: "F. Scott Fitzgerald",
            },
          ],
        }
      ]
    },
    {
      id: 2,
      name: "Book Fair 2022",
      description: "The annual international book fair at the University of Helsinki",
      dateCreated: "2022-02-01",
      books: [
        {
          id: 5,
          title: "The Night Circus",
          subtitle: "J.D. Salinger",
          description: "A classic novel by Harper Lee",
          firstPublishDate: "1951-07-11",
          coverImg: "https://example.com/night-circus.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 1,
              name: "J.D. Salinger",
            },
          ],
        },
        {
          id: 6,
          title: "To Kill a Mockingbird",
          subtitle: "Harper Lee",
          description: "A classic novel by Harper Lee",
          firstPublishDate: "1960-07-11",
          coverImg: "https://example.com/to-kill-a-mockingbird.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 1,
              name: "Harper Lee",
            },
          ],
        },
        {
          id: 7,
          title: "1984",
          subtitle: "George Orwell",
          description: "A dystopian novel by George Orwell",
          firstPublishDate: "1949-06-01",
          coverImg: "https://example.com/1984.jpg",
          subjects: ["Fiction", "Dystopian"],
          authors: [
            {
              id: 2,
              name: "George Orwell",
            },
          ],
        },
        {
          id: 8,
          title: "The Great Gatsby",
          subtitle: "F. Scott Fitzgerald",
          description: "A classic novel by F. Scott Fitzgerald",
          firstPublishDate: "1925-02-19",
          coverImg: "https://example.com/the-great-gatsby.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 3,
              name: "F. Scott Fitzgerald",
            },
          ],
        },
        {
          id: 9,
          title: "The Catcher in the Rye",
          subtitle: "J.D. Salinger",
          description: "A classic novel by Harper Lee",
          firstPublishDate: "1951-07-11",
          coverImg: "https://example.com/catcher-in-the-rye.jpg",
          subjects: ["Fiction", "Classic"],
          authors: [
            {
              id: 1,
              name: "J.D. Salinger",
            },
          ],
        },
      ]
    },
    {
      id: 3,
      name: "Book Fair 2023",
      description: "The annual international book fair at the University of Helsinki",
      dateCreated: "2023-01-01",
      books: []
    }
  ]
}
