export const parseDate = (date:string) => {
  return new Date(date).toLocaleDateString("ru-RU");
}
