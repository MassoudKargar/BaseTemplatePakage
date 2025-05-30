﻿namespace BaseTemplate.Core.Resources;

public sealed class ProjectValidationError
{
    #region Global
    //مقدار {0} اجباری می باشد
    public const string VALIDATION_ERROR_REQUIRED = nameof(VALIDATION_ERROR_REQUIRED);

    public const string VALIDATION_ERROR_NOT_EXIST = nameof(VALIDATION_ERROR_NOT_EXIST);

    public const string VALIDATION_ERROR_NOT_EXIST_ANY = nameof(VALIDATION_ERROR_NOT_EXIST_ANY);

    public const string VALIDATION_ERROR_DUPLICATE = nameof(VALIDATION_ERROR_DUPLICATE);

    public const string VALIDATION_ERROR_NOT_VALID = nameof(VALIDATION_ERROR_NOT_VALID);

    public const string VALIDATION_ERROR_NOT_EQUAL_TO = nameof(VALIDATION_ERROR_NOT_EQUAL_TO);

    public const string VALIDATION_ERROR_FORMAT = nameof(VALIDATION_ERROR_FORMAT);

    //حذف {0} استفاده شده امکان پذیر نمی باشد
    public const string VALIDATION_ERROR_NOT_POSSIBLE_TO_DELETE_USED_ITEM = nameof(VALIDATION_ERROR_NOT_POSSIBLE_TO_DELETE_USED_ITEM);

    public const string VALIDATION_ERROR_CHANGE_STATUS = nameof(VALIDATION_ERROR_CHANGE_STATUS);
    #endregion

    #region Number
    public const string VALIDATION_ERROR_NUMBER_BETWEEN = nameof(VALIDATION_ERROR_NUMBER_BETWEEN);

    public const string VALIDATION_ERROR_NUMBER_LESS_THAN = nameof(VALIDATION_ERROR_NUMBER_LESS_THAN);
    public const string VALIDATION_ERROR_NUMBER_LESS_THAN_OR_EQUAL_THAN = nameof(VALIDATION_ERROR_NUMBER_LESS_THAN_OR_EQUAL_THAN);

    public const string VALIDATION_ERROR_NUMBER_GRATER_THAN = nameof(VALIDATION_ERROR_NUMBER_GRATER_THAN);
    public const string VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN = nameof(VALIDATION_ERROR_NUMBER_GRATER_OR_EQUAL_THAN);

    public const string VALIDATION_ERROR_MUST_BE_NUMERIC = nameof(VALIDATION_ERROR_MUST_BE_NUMERIC);
    #endregion

    #region String
    //طول مناسب برای {0} بزرگ تر برای {1} و کوچک تر برابر {2} می باشد
    public const string VALIDATION_ERROR_STRING_LENGTH_BETWEEN = nameof(VALIDATION_ERROR_STRING_LENGTH_BETWEEN);

    public const string VALIDATION_ERROR_STRING_MIN_LENGTH = nameof(VALIDATION_ERROR_STRING_MIN_LENGTH);

    public const string VALIDATION_ERROR_STRING_MAX_LENGTH = nameof(VALIDATION_ERROR_STRING_MAX_LENGTH);

    public const string VALIDATION_ERROR_STRING_LENGTH_MUST_EQUAL = nameof(VALIDATION_ERROR_STRING_LENGTH_MUST_EQUAL);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_UPPER_CASE = nameof(VALIDATION_ERROR_STRING_MUST_HAS_UPPER_CASE);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_LOWER_CASE = nameof(VALIDATION_ERROR_STRING_MUST_HAS_LOWER_CASE);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_DIGIT = nameof(VALIDATION_ERROR_STRING_MUST_HAS_DIGIT);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_NON_ALPHA_NUMERIC = nameof(VALIDATION_ERROR_STRING_MUST_HAS_NON_ALPHA_NUMERIC);

    public const string VALIDATION_ERROR_STRING_MUST_HAS_UNIQUE_CHAR = nameof(VALIDATION_ERROR_STRING_MUST_HAS_UNIQUE_CHAR);

    public const string VALIDATION_ERROR_INVLAID_IP_ADDRESS = nameof(VALIDATION_ERROR_INVLAID_IP_ADDRESS);
    #endregion

    #region Date
    public const string VALIDATION_ERROR_DATE_LESS_THAN = nameof(VALIDATION_ERROR_DATE_LESS_THAN);

    public const string VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL = nameof(VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL);

    public const string VALIDATION_ERROR_DATE_LESS_THAN_TO_TODAY = nameof(VALIDATION_ERROR_DATE_LESS_THAN_TO_TODAY);

    public const string VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL_TO_TODAY = nameof(VALIDATION_ERROR_DATE_LESS_THAN_OR_EQUAL_TO_TODAY);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN = nameof(VALIDATION_ERROR_DATE_GREATER_THAN);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL = nameof(VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN_TO_TODAY = nameof(VALIDATION_ERROR_DATE_GREATER_THAN_TO_TODAY);

    public const string VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL_TO_TODAY = nameof(VALIDATION_ERROR_DATE_GREATER_THAN_OR_EQUAL_TO_TODAY);
    #endregion

    #region Password

    public const string NOT_VALID_TO_CHANGE_PASSWORD = nameof(NOT_VALID_TO_CHANGE_PASSWORD);

    public const string INVALID_DATA = nameof(INVALID_DATA);

    #endregion
}