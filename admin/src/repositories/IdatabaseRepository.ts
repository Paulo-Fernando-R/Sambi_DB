export default interface IdatabaseRepository {
  getDatabases(): Promise<string[]>;
  createDatabase(databaseName: string): Promise<void>;
  dropDatabase(databaseName: string): Promise<void>;
}
