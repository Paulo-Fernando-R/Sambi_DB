export default interface IregisterRepository {

    update(databaseName: string, collectionName: string, registerId: string, data: object): Promise<void>;
    delete(databaseName: string, collectionName: string, registerId: string): Promise<void>;
}