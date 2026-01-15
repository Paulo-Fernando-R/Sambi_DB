import { type AxiosInstance } from "axios";

export interface IcustomAxios {
    instance: AxiosInstance;
    init(): Promise<void>;
}