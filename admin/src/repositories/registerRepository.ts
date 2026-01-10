import CustomError from "../errors/customError";
import { type IcustomAxios } from "../services/IcustomAxios";
import IregisterRepository from "./IregisterRepository";


export default class RegisterRepository implements IregisterRepository {
    private axios: IcustomAxios;
    constructor(axios: IcustomAxios) {
        this.axios = axios;
    }

    async delete(databaseName: string, collectionName: string, registerId: string): Promise<void> {

        const request = {
            collectionName: collectionName,
            registerId: registerId,
            confirm: true
        }

        try {
            const response = await this.axios.instance(`/Register/Delete/${databaseName}`, { method: "DELETE", data: request });

            if (response.status !== 200) {
                throw new CustomError("Failed to fetch data", response.status, response.data.toString());
            }

        }
        catch (error) {
            throw new CustomError("Failed to fetch data", 500, error as string);
        }
    }

    async update(databaseName: string, collectionName: string, registerId: string, data: object): Promise<void> {

        const request = {
            collectionName: collectionName,
            registerId: registerId,
            data: data
        }

        try {
            const response = await this.axios.instance(`/Register/Update/${databaseName}`, { method: "PUT", data: request });

            if (response.status !== 200) {
                throw new CustomError("Failed to fetch data", response.status, response.data.toString());
            }

        }
        catch (error) {
            throw new CustomError("Failed to fetch data", 500, error as string);
        }
    }

    async insert(databaseName: string, collectionName: string, data: object): Promise<void> {

        const request = {
            collectionName: collectionName,
            data: data
        }

        try {
            const response = await this.axios.instance(`/Register/Create/${databaseName}`, { method: "POST", data: request });

            if (response.status !== 200) {
                throw new CustomError("Failed to fetch data", response.status, response.data.toString());
            }

        }
        catch (error) {
            throw new CustomError("Failed to fetch data", 500, error as string);
        }
    }


}   