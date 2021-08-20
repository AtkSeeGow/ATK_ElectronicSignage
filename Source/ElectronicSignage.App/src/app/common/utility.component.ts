declare const $: any;

import { IAngularMyDpOptions, IMyStyles } from 'angular-mydatepicker';

export class DatePickerUtility {
  static DatePickerOptions: IAngularMyDpOptions = {
    dateRange: false,
    dateFormat: 'yyyy/mm/dd',
    dayLabels: { su: "週日", mo: "週一", tu: "週二", we: "週三", th: "週四", fr: "週五", sa: "週六" },
    monthLabels: { 1: "一月", 2: "二月", 3: "三月", 4: "四月", 5: "五月", 6: "六月", 7: "七月", 8: "八月", 9: "九月", 10: "十月", 11: "十一月", 12: "十二月" },
    firstDayOfWeek: "mo",
    sunHighlight: true,
    todayTxt: "今天",
    stylesData: {
      selector: 'dp1',
      styles: `
           .dp1 .myDpSelector {
              top: 100px;
           }
         `
    }
  };

  static OnDateFieldChanged(event: any, condition: any, fieldName: string) {
  }
}

export class StringUtility {
  static SeparationSymbolMerge(originValue: string, mergeValue: string): string {
    let result = "";

    if (originValue === null)
      return result;

    const openParenthesisLastIndexOf = originValue.lastIndexOf(',');
    if (openParenthesisLastIndexOf != -1)
      result = originValue.substring(0, openParenthesisLastIndexOf) + ', ' + mergeValue;
    else
      result = mergeValue;

    return result;
  }

  static SelectLastValue(value: string): string {
    let result = "";

    if (value === null)
      return result;

    const openParenthesisLastIndexOf = value.lastIndexOf(',');
    if (openParenthesisLastIndexOf != -1)
      result = value.substring(openParenthesisLastIndexOf + 1, value.length);
    else
      result = value;

    return result.trim();
  }
}
