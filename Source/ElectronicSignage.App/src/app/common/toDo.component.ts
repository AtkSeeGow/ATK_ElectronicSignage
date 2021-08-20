import * as moment from 'moment';

export class ToDo {
    id: string = '00000000-0000-0000-0000-000000000000';
    description: string = '';
    expiryDate: any = '';

    static Clone(toDo: ToDo): ToDo {
        const result = new ToDo();
        result.id = toDo.id;
        result.description = toDo.description;
        if (toDo.expiryDate)
            result.expiryDate = toDo.expiryDate.singleDate.formatted;
        return result;
    }

    static Parse(toDo: ToDo): ToDo {
        if (toDo.expiryDate != null) {
            var expiryDate = moment(toDo.expiryDate);
            toDo.expiryDate = {
                isRange: false,
                singleDate: {
                    date: {
                        year: expiryDate.year(),
                        month: expiryDate.month() + 1,
                        day: expiryDate.date()
                    },
                    formatted: expiryDate.format("YYYY/MM/DD")
                }
            };
        }
        return toDo;
    }
}
