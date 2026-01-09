export default interface IregisterRepository {
    delete(databaseName: string, collectionName: string, registerId: string): Promise<void>;
}