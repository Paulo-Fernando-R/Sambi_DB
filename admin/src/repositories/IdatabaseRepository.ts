export default interface IdatabaseRepository {
  getDatabases(): Promise<string[]>;
}
