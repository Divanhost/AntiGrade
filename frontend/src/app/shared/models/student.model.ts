
export class Student {
    id: number;
    firstName: string;
    lastName: string;
    groupId: number;
    fullName = this.lastName + ' ' + this.firstName;
}
