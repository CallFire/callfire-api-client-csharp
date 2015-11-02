package com.callfire.api.client;

import org.apache.http.client.methods.RequestBuilder;

/**
 * Extend abstract filter in case you need to modify outgoing http requests
 *
 * @since 1.0
 */
public abstract class RequestFilter implements Comparable<RequestFilter> {

    /**
     * Default order number
     */
    public static final int DEFAULT_ORDER = 10;

    /**
     * Configure Apache HTTP request as you need
     *
     * @param requestBuilder HTTP request build
     */
    public abstract void filter(RequestBuilder requestBuilder);

    /**
     * Filters with greater order number are executed first
     *
     * @return order number
     */
    protected int order() {
        return DEFAULT_ORDER;
    }

    @Override
    public int compareTo(RequestFilter o) {
        return Integer.compare(o.order(), order());
    }
}
