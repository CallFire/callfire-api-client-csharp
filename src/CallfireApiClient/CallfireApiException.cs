package com.callfire.api.client;

import com.callfire.api.client.api.common.model.ErrorMessage;
import org.apache.commons.lang3.builder.ToStringBuilder;

/**
 * Callfire API exception is thrown by client in case of 4xx or 5xx HTTP code
 * response
 *
 * @since 1.0
 */
public class CallfireApiException extends RuntimeException {
    protected ErrorMessage apiErrorMessage;

    public CallfireApiException(ErrorMessage apiErrorMessage) {
        this.apiErrorMessage = apiErrorMessage;
    }

    /**
     * Get detailed error message with HTTP code, help link, etc.
     *
     * @return detailed message
     */
    public ErrorMessage getApiErrorMessage() {
        return apiErrorMessage;
    }

    /**
     * Set detailed error message with HTTP code, help link, etc.
     *
     * @param apiErrorMessage detailed message
     */
    public void setApiErrorMessage(ErrorMessage apiErrorMessage) {
        this.apiErrorMessage = apiErrorMessage;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
            .appendSuper(super.toString())
            .append("apiErrorMessage", apiErrorMessage)
            .toString();
    }
}
