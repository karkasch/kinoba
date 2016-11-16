module KinobaData {
    export interface IDataResult {
        total: number;
        data: Array<any>;
    }

    export class SpecialistSearchResult {
        total: number;
        data: Array<KinobaCore.Specialist>;
    }
} 