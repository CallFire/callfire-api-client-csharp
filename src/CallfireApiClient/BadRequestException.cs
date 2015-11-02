package com.callfire.api.client;

import com.callfire.api.client.api.common.model.ErrorMessage;
import org.apache.commons.lang3.builder.ToStringBuilder;

/**
 * Exception thrown in case if platform returns HTTP code 400 - Bad request, the request was formatted improperly.
 *
 * @since 1.5
 */
public class BadRequestException extends CallfireApiException {
    public BadRequestException(ErrorMessage errorMessage) {
        super(errorMessage);
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
            .appendSuper(super.toString())
            .toString();
    }
}
