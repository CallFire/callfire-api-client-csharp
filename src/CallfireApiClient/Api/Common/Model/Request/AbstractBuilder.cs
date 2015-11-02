package com.callfire.api.client.api.common.model.request;

import com.callfire.api.client.api.common.model.CallfireModel;

/**
 * Common builder for request objects
 *
 * @param <R> request type
 */
public class AbstractBuilder<R extends CallfireModel> {
    protected final R request;

    protected AbstractBuilder(R request) {
        this.request = request;
    }

    /**
     * Build request
     *
     * @return find request pojo
     */
    public R build() {
        return request;
    }
}
