export default class CustomError extends Error {
    displayMessage?: string;
    code?: number;
    error?: string;
    constructor(message?: string, code?: number, error?: string) {
        super(message);
        this.displayMessage = message;
        this.code = code;
        this.error = error;
    }
}