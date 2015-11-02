

namespace 

///
/// Contains common fields for finder endpoints
///
public abstract class FindRequest extends CallfireModel {
    protected Long limit;
    protected Long offset;
    protected String fields;

    /**
     * Get max number of records per page to return. If items.size() less than limit assume no more items.
     * If value not set, default is 100
     *
     * @return limit number
     */
    public Long getLimit() {
        return limit;
    }

    /**
     * Get offset to start of page
     * If value not set, default is 0
     *
     * @return offset
     */
    public Long getOffset() {
        return offset;
    }

    /**
     * Get limit fields returned. Example fields=id,items(name,agents(id))
     *
     * @return field to return
     */
    public String getFields() {
        return fields;
    }

    @Override
    public String toString() {
        return new ToStringBuilder(this)
            .append("limit", limit)
            .append("offset", offset)
            .append("fields", fields)
            .toString();
    }

    /**
     * Abstract builder for find requests
     *
     * @param <B> type of builder
     */
    @SuppressWarnings("unchecked")
    public static abstract class FindRequestBuilder<B extends FindRequestBuilder, R extends FindRequest>
        extends AbstractBuilder<R> {

        protected FindRequestBuilder(R request) {
            super(request);
        }

        /**
         * Set max number of items returned.
         *
         * @param limit max number of items
         * @return builder object
         */
        public B limit(Long limit) {
            request.limit = limit;
            return (B) this;
        }

        /**
         * Offset from start of paging source
         *
         * @param offset offset value
         * @return builder object
         */
        public B offset(Long offset) {
            request.offset = offset;
            return (B) this;
        }

        /**
         * Set limit fields returned. Example fields=id,items(name,agents(id))
         *
         * @param fields fields to return
         * @return builder object
         */
        public B fields(String fields) {
            request.fields = fields;
            return (B) this;
        }
    }
}
