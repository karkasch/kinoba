module KinobaCore {
    export class School {
        id: number;
        name: string;

        constructor(name: string, id: number = null) {
            this.id = id;
            this.name = name;
        }
                
    }
} 