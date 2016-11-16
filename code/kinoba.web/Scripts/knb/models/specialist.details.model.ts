module KinobaModels {
    export class SpecialistDetailsModel extends kendo.data.ObservableObject {
        specialist: KinobaCore.Specialist;
        isVisible: boolean;

        windowTitle: string;

        constructor() {
            super();
            this.windowTitle = 'Специалист';
        }

        sexIcon() {
            //console.log('sexIcon', this.specialist);
            if (this.specialist.sex == 1)
                return "M";
            else if (this.specialist.sex == 2)
                return "Ж";

            return "";
        }
    }
} 