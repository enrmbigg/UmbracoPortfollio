/// <binding BeforeBuild='sass-compile, fetch-js, minify-css, minify-js' ProjectOpened='watch-sass, watch-js' />
var gulp = require('gulp');
var sass = require('gulp-sass');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var minify = require('gulp-minify-css');

gulp.task('sass-compile', function () {
    gulp.src('./Content/scss/style.scss')
        .pipe(sass({
            includePaths: './node_modules/foundation-sites/scss'
        }))
        .pipe(gulp.dest('./Content/'));
});

gulp.task('fetch-js', function () {
    gulp.src('./node_modules/foundation-sites/dist/foundation.js')
        .pipe(gulp.dest('./Scripts/'));
});
gulp.task('watch-sass', function () {
    gulp.watch('./Content/scss/*.scss', ['sass-compile']);
});

gulp.task('minify-js', function () {
    gulp.src('./Scripts/*.js')
    .pipe(concat('site.js'))
    .pipe(uglify())
    .pipe(gulp.dest('./Scripts/Build/'));
});

gulp.task('minify-css', function () {
    gulp.src('./Content/*.css')
    .pipe(concat('site.css'))
    .pipe(minify())
    .pipe(gulp.dest('./Content/Build/'));
});

gulp.task('watch-js', function () {
    gulp.watch('./Scripts/*.js', ['minify-js']);
});